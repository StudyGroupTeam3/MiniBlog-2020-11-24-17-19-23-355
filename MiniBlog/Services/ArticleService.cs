using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Stores;

namespace MiniBlog.Services
{
    public class ArticleService
    {
        private readonly IArticleStore articleStore;

        public ArticleService(IArticleStore articleStore)
        {
            this.articleStore = articleStore;
        }

        public Article FindByID(Guid id)
        {
            return articleStore.Articles.FirstOrDefault(article => article.Id == id);
        }
    }
}
