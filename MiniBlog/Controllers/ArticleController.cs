using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public List<Article> List()
        {
            return articleService.GetArticleList();
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            articleService.Register(article);

            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            return articleService.FoundArticleById(id);
        }
    }
}