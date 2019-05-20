using System;
using Repository.Application.DataModel;
using Model.Application;
namespace Repository.Application
{
    public class UnitOfWork : IDisposable

    {
        private DB_TritsurEntities dbTritsur = new DB_TritsurEntities();

         private GenericRepository<AspNetUsers> aspNetUserRepository;
        private bool disposed = false;

       
   
        public GenericRepository<AspNetUsers> AspNetUserRepository
        {
            get
            {
                if (aspNetUserRepository == null)
                {
                    aspNetUserRepository = new GenericRepository<AspNetUsers>(dbTritsur);
                }
                return aspNetUserRepository;
            }

            set
            {
                AspNetUserRepository = value;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbTritsur.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
