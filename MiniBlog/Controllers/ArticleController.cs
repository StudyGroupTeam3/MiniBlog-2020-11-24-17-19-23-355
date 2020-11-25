using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private ArticleService articleService;

        public ArticleController(ArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public List<Article> List()
        {
            return articleService.GetAllArticles();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            articleService.AddArticle(article);
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            return articleService.FindArticleByID(id);
        }
    }
}