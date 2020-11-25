using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;

namespace MiniBlog.Services
{
    public class ArticleService
    {
        private IUserStore userStore;
        private IArticleStore articleStore;
        private UserService userService;

        public ArticleService(IUserStore userStore, IArticleStore articleStore, UserService userService)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
            this.userService = userService;
        }

        public void AddArticle(Article article)
        {
            if (article.UserName != null)
            {
                userService.RegisterUser(new User(article.UserName));
                articleStore.Articles.Add(article);
            }
        }
    }
}
