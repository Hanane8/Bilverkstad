namespace DataLager
{
    public class UnitOfWork
    {
        private PersonRepository _personRepository;
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
        public PersonRepository PersonRepo
        {
            get
            {
                if (_personRepository == null)
                    _personRepository = new PersonRepository(_dbContext);
                return _personRepository;
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
