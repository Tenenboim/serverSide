using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BLL
{
    public class Convertions
    {
        public static User UserDtoToDAL(UserDTO u)
        {
            return new User
            {
                UserId = u.UserId,
                UserFullName = u.UserFullName,
                UserAddress = u.UserAddress,
                UserEmail = u.UserEmail,
                UserPassword = u.UserPassword,
                UserPhone = u.UserPhone,
                UserRecognizeName = u.UserRecognizeName,
                RoleId = u.RoleId

            };
        }
        public static UserDTO UserToDto(User u)
        {
            return new UserDTO
            {
                UserId = u.UserId,
                UserFullName = u.UserFullName,
                UserAddress = u.UserAddress,
                UserEmail = u.UserEmail,
                UserPassword = u.UserPassword,
                UserPhone = u.UserPhone,
                UserRecognizeName = u.UserRecognizeName,
                RoleId = u.RoleId
            };
        }
        public static Role RoleDtoToDAL(RoleDTO r)
        {
            return new Role
            {
                  RoleId=r.RoleId,
                   RoleName=r.RoleName
            };
        }
        public static RoleDTO RoleToDto(Role r)
        {
            return new RoleDTO
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            };
        }
        public static Product ProductDtoToDAL(ProductDTO p)
        {
            return new Product
            {
                ProductDescription = p.ProductDescription,
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UserId = p.UserId,
                CategoryId = p.CategoryId,
                AddressPointX = p.AddressPointX,
                AddressPointY = p.AddressPointY,
                AddressDescription=p.AddressDescription,
                DateFound = p.DateFound,
                LostOrFound = p.LostOrFound,
                WasDone = p.WasDone,
                CleverAgent=p.CleverAgent
            };
        }

        public static ProductDTO ProductToDto(Product p)
        {
            return new ProductDTO
            {
                ProductDescription = p.ProductDescription,
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UserId = p.UserId,
                CategoryId = p.CategoryId,
                AddressPointX = p.AddressPointX,
                AddressPointY = p.AddressPointY,
                AddressDescription = p.AddressDescription,
                DateFound = p.DateFound,
                LostOrFound = p.LostOrFound,
                WasDone = p.WasDone,
                CleverAgent = p.CleverAgent
            };
        }
        public static Parameter ParameterDtoToDAL(ParameterDTO p)
        {
            return new Parameter
            {
                ParameterId = p.ParameterId,
                ParameterName = p.ParameterName,
                CategoryId = p.CategoryId
            };
        }

        public static ParameterDTO ParameterToDto(Parameter p)
        {
            return new ParameterDTO
            {
                ParameterId = p.ParameterId,
                ParameterName = p.ParameterName,
                CategoryId = p.CategoryId
            };
        }
        public static ParameteOfProduct ParameterOfProductDtoToDAL(ParameterOfProductDTO p)
        {
            return new ParameteOfProduct
            {
                 ParameterId=p.ParameterId,
                 ProductId=p.ProductId,
                  Value=p.Value
            };
        }

        public static ParameterOfProductDTO ParameterOfProductToDto(ParameteOfProduct p)
        {
            return new ParameterOfProductDTO
            {
                ParameterId = p.ParameterId,
                ProductId = p.ProductId,
                Value = p.Value
            };
        }
        public static Matching MatchingDtoToDAL(MatchingDTO m)
        {
            return new Matching
            {
                LostProductId=m.LostProductId,
                FindProductId =m.FindProductId,
                Relevant =m.Relevant
            };
        }

        public static MatchingDTO MatchingToDto(Matching m)
        {
            return new MatchingDTO
            {
                LostProductId = m.LostProductId,
                FindProductId = m.FindProductId,
                Relevant = m.Relevant
            };
        }

        public static Category CategoryDtoToDAL(CategoryDTO c)
        {
            return new Category
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                ParentId = c.ParentId
            };
        }

        public static CategoryDTO CategoryToDto(Category c)
        {
            return new CategoryDTO
            {

                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                ParentId = c.ParentId
            };
        }
    }
}
