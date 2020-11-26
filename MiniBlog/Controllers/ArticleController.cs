using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService articleService;

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
            articleService.CreateArticle(article);

            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            return articleService.FoundArticle(id);
        }
    }
}