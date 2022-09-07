using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackSearch.DAL
{
    public class ImageRepository
    {
        private readonly DataContext context;
        private readonly DbSet<ImageObj> dbImages;

        public ImageRepository(DataContext dataContext)
        {
            this.context = dataContext;
            dbImages = dataContext.Set<ImageObj>();
        }
        public async Task<ImageObj> InsertAsync(ImageObj image)
        {
            await dbImages.AddAsync(image);
            context.SaveChanges();
            return image;
        }
        public async Task<ImageObj> UpdateAsync(int Id, ImageObj image)
        {
            ImageObj img = dbImages.FirstOrDefault(i => i.Id == Id);
            if (img != null)
            {
                img.Url = image.Url;
                img.Description = image.Description;
                context.SaveChanges();
            }
            return image;
        }
        public async Task DeleteAsync(int Id)
        {
            dbImages.Remove(dbImages.FirstOrDefault(i => i.Id == Id));
            context.SaveChanges();
        }
        public IEnumerable<ImageObj> GetAll()
        {
            return dbImages;
        }

        public IEnumerable<ImageObj> GetByFilter(string filter)
        {
            if (filter != string.Empty && filter != null)
            {
                return dbImages.Where(i => i.Description.ToLower().Contains(filter.ToLower()));
            }

            return dbImages;
        }
        public ImageObj? GetById(int id)
        {
            return dbImages.FirstOrDefault(i => i.Id == id);
        }
    }
}
