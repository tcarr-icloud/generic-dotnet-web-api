namespace webapi;

public class UsersApi : IDisposable
{
    private readonly DatabaseApiContext _databaseApiContext;
    private GenericRepository<Company> _companyRepository;
    private bool _disposed;
    private GenericRepository<User> _userRepository;
    private GenericRepository<Workspace> _workspaceRepository;

    public UsersApi(DatabaseApiContext databaseApiContext)
    {
        _databaseApiContext = databaseApiContext;
    }

    public GenericRepository<User> UserRepository
    {
        get
        {
            _userRepository ??= new GenericRepository<User>(_databaseApiContext);
            return _userRepository;
        }
    }

    public GenericRepository<Company> CompanyRepository
    {
        get
        {
            _companyRepository ??= new GenericRepository<Company>(_databaseApiContext);
            return _companyRepository;
        }
    }

    public GenericRepository<Workspace> WorkspaceRepository
    {
        get
        {
            _workspaceRepository ??= new GenericRepository<Workspace>(_databaseApiContext);
            return _workspaceRepository;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Save()
    {
        _databaseApiContext.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _databaseApiContext.Dispose();

        _disposed = true;
    }
}