using Microsoft.EntityFrameworkCore;

namespace webapi.Data
{
    internal static class PostsRepository
    {
        internal static async Task<bool> CreatePostAsync(Post post)
        {
            using AppDBContext db = new();
            try
            {
                db.Posts.AddAsync(post);

                return await db.SaveChangesAsync() >= 1;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal static async Task<bool> DeletePostAsync(int postId)
        {
            using AppDBContext db = new();
            try
            {
                Post postToDelete = await GetPostByIdAsync(postId);

                db.Posts.Remove(postToDelete);

                return await db.SaveChangesAsync() >= 1;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal static async Task<Post> GetPostByIdAsync(int postId)
        {
            using AppDBContext db = new();
            return await db.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
        }

        internal static async Task<List<Post>> GetPostsAsync()
        {
            using AppDBContext db = new();
            return await db.Posts.ToListAsync();
        }

        internal static async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            using var db = new AppDBContext();
            try
            {
                db.Posts.Update(postToUpdate);

                return await db.SaveChangesAsync() >= 1;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}