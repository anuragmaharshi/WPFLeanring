using LearnWpf.SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LearnWpf.Services
{
    public class TopicAndAreaRepository : ITopicAndAreaRepository
    {
        DataLayerContext _context = new DataLayerContext();
        public async Task<TopicsAndArea> AddTopicOrAreaAsync(TopicsAndArea topicOrArea)
        {
            _context.TopicsAndAreas.Add(topicOrArea);
            await _context.SaveChangesAsync();
            return topicOrArea;
        }

        public async Task DeleteTopicOrAreaAsync(long Id)
        {
            var topicAndArea = _context.TopicsAndAreas.FirstOrDefault(c => c.Id == Id);
            if (topicAndArea != null)
            {
                _context.TopicsAndAreas.Remove(topicAndArea);
            }
            await _context.SaveChangesAsync();
        }

        public Task<List<TopicsAndArea>> GetTopicAndAreasAsync()
        {
            return _context.TopicsAndAreas.ToListAsync();
        }

        public Task<TopicsAndArea> GetTopicAndAreaAsync(long id)
        {
            return _context.TopicsAndAreas.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<TopicsAndArea> UpdateTopicAndAreaAsync(TopicsAndArea topicOrArea)
        {
            if (!_context.TopicsAndAreas.Local.Any(c => c.Id == topicOrArea.Id))
            {
                _context.TopicsAndAreas.Attach(topicOrArea);
            }
            _context.Entry(topicOrArea).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return topicOrArea;
        }
    }
}
