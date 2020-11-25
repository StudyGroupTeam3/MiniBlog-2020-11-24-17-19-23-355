﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private IArticleStore articleStore;
        public ArticleController(IArticleStore articleStore)
        {
            this.articleStore = articleStore;
        }

        [HttpGet]
        public List<Article> List()
        {
            return articleStore.Articles.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!UserStoreWillReplaceInFuture.Users.Exists(user => article.UserName == user.Name))
                {
                    UserStoreWillReplaceInFuture.Users.Add(new User(article.UserName));
                }

                articleStore.Articles.Add(article);
            }

            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            var foundArticle = ArticleStoreWillReplaceInFuture.Articles.FirstOrDefault(article => article.Id == id);
            return foundArticle;
        }
    }
}