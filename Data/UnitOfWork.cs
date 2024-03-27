namespace webapi;

public class UnitOfWork : IDisposable
{
    private readonly DatabaseApiContext _context;
    private GenericRepository<Company> _companyRepository;

    private bool _disposed;
    private GenericRepository<User> _userRepository;
    private GenericRepository<Workspace> _workspaceRepository;

    public UnitOfWork(DatabaseApiContext context)
    {
        _context = context;
    }

    public GenericRepository<User> UserRepository
    {
        get
        {
            _userRepository ??= new GenericRepository<User>(_context);
            return _userRepository;
        }
    }

    public GenericRepository<Company> CompanyRepository
    {
        get
        {
            _companyRepository ??= new GenericRepository<Company>(_context);
            return _companyRepository;
        }
    }

    public GenericRepository<Workspace> WorkspaceRepository
    {
        get
        {
            _workspaceRepository ??= new GenericRepository<Workspace>(_context);
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
        _context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();

        _disposed = true;
    }
}