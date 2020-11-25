using MiniBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public interface IArticleService
    {
        void Register(Article article);
        Article FoundArticleById(Guid id);
        List<Article> GetArticleList();
    }
}
