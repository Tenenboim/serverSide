using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{

   public static class UserFunctions
    {
        static  Entities db = new Entities();
        //null פונקצית כניסה האם המשתמש קים ומחזירה את המשתמש שנמצא או 
        public static DTO.UserDTO Login(string UserPhone,string UserPassword)
        {

            DAL.User u1 = db.Users.FirstOrDefault(u => u.UserPhone == UserPhone&&u.UserPassword==UserPassword );
            if (u1 != null)
            {
                return BLL.Convertions.UserToDto(u1);
            }
            return null;

        }
        //פונקצית הרשמת משתמש
        public static DTO.UserDTO Register(DTO.UserDTO user)
        {
            DAL.User u1 = BLL.Convertions.UserDtoToDAL(user);
            if (Login(u1.UserPhone,u1.UserPassword) == null)
            {
                u1.RoleId = 3;
                db.Users.Add(u1);
                db.SaveChanges();
                return BLL.Convertions.UserToDto(u1);

            }
            else
            {
                return null;
            }

        }
        //פונקציית שליפת משתמש
        public static UserDTO getUserbyId(int userId)
        {
            return BLL.Convertions.UserToDto(db.Users.First(p => p.UserId == userId));
        }

        //פונקציה שליפת רשימת כל המשתמשים:
        public static List<DTO.UserDTO> UserList()
        {
            List<DAL.User> u = db.Users.ToList();
            if(u!=null)
            {
                List<DTO.UserDTO> u1=new List<UserDTO>();
                foreach (var item in u)
                {
                    u1.Add(BLL.Convertions.UserToDto(item));
                }
                return u1;
            }
            return null;
        }
        //פונקציה ששומרת את השינויים על עריכת משתמש
       public static UserDTO UpdateEditUser(DTO.UserDTO user)
        {
            DAL.User u1 = BLL.Convertions.UserDtoToDAL(user);
            //בדיקה שאין עוד משתמש עם אותו קוד וסיסמא
            DAL.User u3 = db.Users.FirstOrDefault(u => u.UserPassword == u1.UserPassword 
            && u.UserPhone == u1.UserPhone && u.UserId != u1.UserId);
            if (u3 != null)
                return BLL.Convertions.UserToDto(u1);
            DAL.User u2 = db.Users.FirstOrDefault(u => u.UserId == u1.UserId);
            
            if (u2!=null)
            {
            u2.UserPassword = u1.UserPassword;
            u2.UserPhone = u1.UserPhone;
            u2.UserRecognizeName = u1.UserRecognizeName;
            u2.UserFullName = u1.UserFullName;
            u2.UserEmail = u1.UserEmail;
            u2.UserAddress = u1.UserAddress;
            u2.RoleId = u1.RoleId;
            db.SaveChanges();
                return BLL.Convertions.UserToDto(u2);
            }
            return BLL.Convertions.UserToDto(u1);
        }
        //פונקציה שמוסיפה משתמש או מוקדן
        public static DTO.UserDTO AddMokdanOrUser(DTO.UserDTO user)
        {
            DAL.User u1 = BLL.Convertions.UserDtoToDAL(user);
            if (Login(u1.UserPhone,u1.UserPassword) == null)
            {
                //u1.RoleId = 3;
                db.Users.Add(u1);
                db.SaveChanges();
                return BLL.Convertions.UserToDto(u1);

            }
            else
            {
                return null;
            }

        }
    }
}
