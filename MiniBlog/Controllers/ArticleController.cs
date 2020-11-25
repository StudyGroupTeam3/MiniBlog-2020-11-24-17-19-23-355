using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private IUserStore userStore;

        public ArticleController(IArticleStore articleStore, IUserStore userStore)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
        }

        [HttpGet]
        public List<Article> List()
        {
            return ArticleStoreWillReplaceInFuture.Articles.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            if (article.UserName != null)
            {
                if (!UserStoreWillReplaceInFuture.Users.Exists(x => article.UserName == x.Name))
                {
                    userStore.Users.Add(new User(article.UserName));
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