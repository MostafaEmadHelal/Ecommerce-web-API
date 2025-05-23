using AutoMapper;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastracture.Data;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastracture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private readonly IImageManagementService imageManagementService;
        private readonly IMapper mapper;
        private readonly IConnectionMultiplexer connectionMultiplexer;
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateToken generateToken;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public ICustomerBasketRepository CustomerBasketRepository { get; }
       public  IAuth Auth { get; }

        public UnitOfWork(AppDbContext _context,IImageManagementService imageManagementService,IMapper mapper,IConnectionMultiplexer connectionMultiplexer,UserManager<AppUser> userManager,IEmailService emailService,
            SignInManager<AppUser> signInManager,IGenerateToken generateToken
            )
        {
            context = _context;
            this.imageManagementService = imageManagementService;
            this.mapper = mapper;
            this.connectionMultiplexer = connectionMultiplexer;
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.generateToken = generateToken;
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context,mapper,imageManagementService);
            PhotoRepository = new PhotoRepository(context);
            CustomerBasketRepository = new CustomerBasketRepository(connectionMultiplexer);
            Auth = new AuthRepository(userManager,emailService,signInManager,generateToken);
        }
    }
}
