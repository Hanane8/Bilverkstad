namespace DataLager
{
    public class UnitOfWork
    {
        private KundRepository _kundRepository;
        private readonly EntityFramework _dbContext;
        private BokningsRepository _bokningsRepository;
        public UnitOfWork(EntityFramework dbContext)
        {
            this._dbContext = dbContext;
        }

        public void EnsureCreated()
        {
            _dbContext.Database.EnsureCreated();
        }
        public KundRepository KundRepo
        {
            get
            {
                if (_kundRepository == null)
                    _kundRepository = new KundRepository(_dbContext);
                return _kundRepository;
            }
        }

        public BokningsRepository BokningsRepo
        {
            get
            {
                if (_bokningsRepository == null)
                    _bokningsRepository = new BokningsRepository(_dbContext);
                return _bokningsRepository;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
