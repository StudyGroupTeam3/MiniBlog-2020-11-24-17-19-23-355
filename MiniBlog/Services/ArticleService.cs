using MiniBlog.Model;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniBlog.Services
{
    public class ArticleService
    {
        private readonly IArticleStore articleStore;
        private readonly UserService userService;
        public ArticleService(IArticleStore articleStore, UserService userService)
        {
            this.articleStore = articleStore;
            this.userService = userService;
        }

        public List<Article> GetAllArticles()
        {
            return articleStore.Articles;
        }

        public Article FoundArticle(Guid id)
        {
            return articleStore.Articles.FirstOrDefault(article => article.Id == id);
        }

        public Article CreateArticle(Article article)
        {
            if (article.UserName != null)
            {
                userService.Register(new User(article.UserName));
                articleStore.Articles.Add(article);
            }

            return article;
        }
    }
}
