using SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf.Services
{
    public interface ITopicAndAreaRepository
    {
        Task<List<TopicsAndArea>> GetTopicAndAreasAsync();
        Task<TopicsAndArea> GetTopicAndAreaAsync(int id);
        Task<TopicsAndArea> AddTopicOrAreaAsync(TopicsAndArea topicOrArea);
        Task<TopicsAndArea> UpdateTopicAndAreaAsync(TopicsAndArea topicOrArea);
        Task DeleteTopicOrAreaAsync(int Id);
    }
}
