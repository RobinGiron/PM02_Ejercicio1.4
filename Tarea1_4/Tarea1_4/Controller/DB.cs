using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Tarea1_4.Models;

namespace Tarea1_4.Controller
{
    public class DB
    {
        readonly SQLiteAsyncConnection db;
        public DB(string path)
        {
            db = new SQLiteAsyncConnection(path);
            db.CreateTableAsync<IMG>();
        }
        #region CRUD
        public Task<int> insertUpdateFoto(IMG img)
        {
            if (img.id != 0)
            {
                return db.UpdateAsync(img);
            }
            else
            {
                return db.InsertAsync(img);
            }
        }
        public Task<List<IMG>> getListFotos()
        {
            return db.Table<IMG>().ToListAsync();
        }

        public Task<IMG> getFoto(int id)
        {
            return db.Table<IMG>()
                .Where(i => i.id == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> delFoto(IMG img)
        {
            return db.DeleteAsync(img);
        }

        #endregion CRUD
    }
}
