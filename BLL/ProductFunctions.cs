using System.Collections.Generic;
using System.Linq;
using DTO;
using DAL;
using BLL.customClasses;
using System.Drawing;
using System.Net;
using System.IO;
using System;
using System.Web.Script.Serialization;

namespace BLL
{

    public static class ProductFunctions
    {
        static Entities db = new Entities();
     
        public static int FunctionsToaddNewProduct(DTO.ProductDTO productdDto
            , List<DTO.ParameterOfProductDTO> ParameterOfProductAreExistDto,
            List<DTO.ParameterDTO> NewParametersDto,
            List<DTO.ParameterOfProductDTO> NewParameterOfProductDto)

        {
            DAL.Product productdDAL = BLL.Convertions.ProductDtoToDAL(productdDto);
            List<DAL.ParameteOfProduct> ParameterOfProductAreExistDAL = new List<ParameteOfProduct>();
            List<DAL.Parameter> NewParametersDAL = new List<Parameter>();
            List<DAL.ParameteOfProduct> NewParameterOfProductDAL = new List<ParameteOfProduct>();
            //המרת האוביקטים לסוג DAL
            foreach (var item in ParameterOfProductAreExistDto)
            {
                ParameterOfProductAreExistDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
            }
            foreach (var item in NewParametersDto)
            {
                NewParametersDAL.Add(BLL.Convertions.ParameterDtoToDAL(item));
            }
            foreach (var item in NewParameterOfProductDto)
            {
                NewParameterOfProductDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
            }
            //DB שליחה לפונקציה שמוסיפה את המוצר ל
            addNewProduct(productdDAL);

            //של המוצר שהוספנו ID קבלת ה
            int productId = productdDAL.ProductId;

            //שליחה לפונקציה שמוסיפה רשומות לטבלת פרמטרים למוצר
            //אלו בעצם פרמטרים הקיימים כבר בטבלת פרמטרים 
            //כלומר שם הפרמטר קוד קטגוריה וכו' כבר קיים
            //ועכשיו אני יוצרת קשר בין הפרמטר מטבלת פרמטרים למוצר מטבלת מוצרים
            //ParameterOfProduct ע"י הוספת רשומות לטבלת הקשר 
            AddParameterOfProductAreExist(ParameterOfProductAreExistDAL, productId);

            //שליחה לפונקציה המוסיפה פרמטרים חדשים לטבלת פרמטר
            //parameterOfProduct וכן מוסיפה רשומות חדשות לטבלת 
            AddNewParametersAndNewParametersOfProduct(NewParametersDAL, NewParameterOfProductDAL, productId);
            return productId;
        }
        //הוספת פרמטרים חדשים לטבלת פרמטרים וכן לטבלת פרמטרים למוצר 
        private static void AddNewParametersAndNewParametersOfProduct(List<Parameter> newParametersDAL,
            List<ParameteOfProduct> newParameterOfProductDAL, int productId)
        {
            for (int i = 0; i < newParametersDAL.Count(); i++)
            {
                if (newParametersDAL[i].ParameterName!=null&&newParameterOfProductDAL[i].Value != null)
                {
                    db.Parameters.Add(newParametersDAL[i]);
                    db.SaveChanges();
                    newParameterOfProductDAL[i].ParameterId = newParametersDAL[i].ParameterId;
                    newParameterOfProductDAL[i].ProductId = productId;
                    db.ParameteOfProducts.Add(newParameterOfProductDAL[i]);
                }
            }
        }

        public static ProductDTO getProduct(int productId)
        {
            return BLL.Convertions.ProductToDto(db.Products.First(p => p.ProductId == productId));
        }



        //value,productId הוספה לטבלת פרמטרים למוצר את הפרמטרים שהיו כבר קיימים כלומר חדש רק ה 
        private static void AddParameterOfProductAreExist(List<ParameteOfProduct> 
            parameterOfProductAreExistDAL,
            int productId)
        {
            foreach (var item in parameterOfProductAreExistDAL)
            {
                if (item.Value != null)
                {
                    item.ProductId = productId;
                    db.ParameteOfProducts.Add(item);
                }

            }
        }

        //הוספת המוצר
        public static void addNewProduct(DAL.Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();

        }
        public static ProductDTO FunctionsToEditProduct(ProductDTO product, List<ParameterOfProductDTO> 
            parameterOfProductAreExist,
            List<ParameterDTO> newParameters, List<ParameterOfProductDTO> newParameterOfProduct,
            List<ParametersWithParametersOfProducts> parametersOfCategoryWithParametersOfProduct)
        {
            product = UpdateEditProduct(product);
            List<DAL.Parameter> NewParametersDAL = new List<Parameter>();
            List<DAL.ParameteOfProduct> NewParameterOfProductDAL = new List<ParameteOfProduct>();
            //המרת האוביקטים לסוג DAL

            foreach (var item in newParameters)
            {
                NewParametersDAL.Add(BLL.Convertions.ParameterDtoToDAL(item));
            }
            foreach (var item in newParameterOfProduct)
            {
                NewParameterOfProductDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
            }
            //========================

            //parametersOfCategoryWithParametersOfProduct בדיקה האם יש נתונים באוביקט
            //parametersOfCategoryWithParametersOfProduct שנמצאים ב parameteOfProduct כלומר האם לא שינו את הקטגוריה של הפריט ולכן צריך רק לעדכן את הערכים של ה 
            if (parametersOfCategoryWithParametersOfProduct.Count() > 0)
            {
                EditParametersOfCategoryWithParametersOfProduct(parametersOfCategoryWithParametersOfProduct, product.ProductId);
            }
            else
            {
                List<DAL.ParameteOfProduct> ParameterOfProductAreExistDAL = new List<ParameteOfProduct>();
                //מהפרמטרים הקשורים למוצר שנערך כרגע parameterOfProduct ריקון טבלת 
                deleteItemsFromParametersOfProductTable(product.ProductId);
                foreach (var item in parameterOfProductAreExist)
                {
                    ParameterOfProductAreExistDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
                    AddParameterOfProductAreExist(ParameterOfProductAreExistDAL, product.ProductId);
                }

            }

            AddNewParametersAndNewParametersOfProduct(NewParametersDAL, NewParameterOfProductDAL, product.ProductId);
            db.SaveChanges();
            return product;
        }


        //שלהם value  עריכת הפרמטרים של המוצר בעצם עריכת ה
        //נערך מחדש value אלו פרמטרים של מוצר שהוכנסו כבר קודם ועכשיו ה
        //הם נמצאים באוביקט מטיפוס של מחלקה שיצרתי המכילה קוד קטגוריה קוד פרמטר שם פרמטר וערך הפרמטר
        private static void EditParametersOfCategoryWithParametersOfProduct(List<ParametersWithParametersOfProducts> 
            parametersOfCategoryWithParametersOfProduct, int productId)
        {
            //מהפרמטרים הקשורים למוצר שנערך כרגע parameterOfProduct ריקון טבלת 
            deleteItemsFromParametersOfProductTable(productId);
            for (int i = 0; i < parametersOfCategoryWithParametersOfProduct.Count(); i++)
            {
                if (parametersOfCategoryWithParametersOfProduct[i].ParameterOfProductValue != null)
                    db.ParameteOfProducts.Add(new ParameteOfProduct { ParameterId = parametersOfCategoryWithParametersOfProduct[i].ParameterId, ProductId = productId, Value = parametersOfCategoryWithParametersOfProduct[i].ParameterOfProductValue });
            }
        }

        private static void deleteItemsFromParametersOfProductTable(int productId)
        {
            db.ParameteOfProducts.RemoveRange(db.ParameteOfProducts.Where(p => p.ProductId == productId));
            db.SaveChanges();
        }

        //עריכת המוצר
        public static ProductDTO UpdateEditProduct(DTO.ProductDTO product)
        {
            DAL.Product u1 = BLL.Convertions.ProductDtoToDAL(product);
            DAL.Product u2 = db.Products.FirstOrDefault(u => u.ProductId == u1.ProductId);
            if (u2 != null)
            {
                u2.ProductName = u1.ProductName;
                u2.CategoryId = u1.CategoryId;
                u2.ProductDescription = u1.ProductDescription;
                u2.AddressPointX = u1.AddressPointX;
                u2.AddressPointY = u1.AddressPointY;
                u2.AddressDescription = u1.AddressDescription;
                u2.DateFound = u1.DateFound;
                u2.CleverAgent = u1.CleverAgent;
                u2.WasDone = u1.WasDone;


                db.SaveChanges();
                return BLL.Convertions.ProductToDto(u2);
            }
            return BLL.Convertions.ProductToDto(u1);
        }

        //מחזירה את כל האבדות  userId==0 פונקציה שמחזירה את האבדות של המשתמש שקיבלה אם ה
        public static List<DTO.ProductDTO> getLosts(int userId)
        {
            List<DTO.ProductDTO> losts = new List<ProductDTO>();
            if (userId == 0)
                db.Products.Where(p => !p.WasDone && p.LostOrFound).ToList().ForEach(d => losts.Add(BLL.Convertions.ProductToDto(d)));
            else
                db.Products.Where(p => !p.WasDone && p.UserId == userId && p.LostOrFound).ToList().ForEach(d => losts.Add(BLL.Convertions.ProductToDto(d)));
            return losts;
        }

        //מחזירה את כל המציאות userId==0 פונקציה שמחזירה את המציאות של המשתמש שקיבלה אם ה
        public static List<DTO.ProductDTO> getFounds(int userId)
        {
            List<DTO.ProductDTO> founds = new List<ProductDTO>();
            if (userId == 0)
                db.Products.Where(p => !p.WasDone && !p.LostOrFound).ToList().ForEach(d => founds.Add(BLL.Convertions.ProductToDto(d)));
            else
                db.Products.Where(p => !p.WasDone && p.UserId == userId && !p.LostOrFound).ToList().ForEach(d => founds.Add(BLL.Convertions.ProductToDto(d)));
            return founds;
        }


        /// <summary>
        /// פונקציה המוצאת התאמות למוצר שקיבלה את הקוד שלו
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>List<BLL.customClasses.ClassForMatches></returns>
        public static List<BLL.customClasses.ClassForMatches> search(int productId)
        {
            DAL.Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            List<DAL.Product> matchesDAL = new List<Product>();
            List<BLL.customClasses.ClassForMatches> matches = new List<BLL.customClasses.ClassForMatches>();
            db.Products.Where(p =>

            //בדיקה שהחפץ פעיל
            !p.WasDone &&
            
            //בדיקה שהקטגוריות שוות
            p.CategoryId == product.CategoryId &&

            //בדיקה שהחפץ הפוך מהחפץ שקיבלנו את הקוד שלו כלומר שיהיה
            //אבידה=מציאה ולא מציאה=מציאה / אבידה=אבידה 
            p.LostOrFound != product.LostOrFound &&

            //בדיקה שתאריך האבידה לפני תאריך המציאה
            (product.LostOrFound && product.DateFound < p.DateFound ||
            !product.LostOrFound && product.DateFound > p.DateFound) &&

            //בדיקה שלשתי החפצים יש תאור על כתובת האבידה/מציאה
           ((product.AddressDescription != null && p.AddressDescription != null) ||

           // או שלשתי החפצים נשמרו הנקודות על המפה
           product.AddressPointX != null&&p.AddressPointX!=null &&
           // וגם מרחק בין שתי החפצים הגיוני
           //( לא סופי ולא נכון הוא לצורך דוגמא Distance=="20")
           GetDistanceAndDuration(product.AddressPointX,product.AddressPointY
           ,p.AddressPointX,p.AddressPointY).Distance=="20")&&

           //בדיקת ערכי הפרמטרים למוצר ע"י שליחה לפונקציה שעוברת על 
           //ערכי הפרמטרים הקשורים למוצר ובודקת האם יש התאמה בינהם
           checkTheParameters(productId, product.ProductId)

            //matchesDAL במקרה שכל התנאים הנ"ל התקיימו החפץ נוסף ל 
            ).ToList().ForEach(d => matchesDAL.Add(d));

            //קריאה לפונקציה ששולחת מיילים לבעלי החפצים שביקשו לקבל עכונים של "נמצאה התאמה חדשה"
            checkIfSendEmails(matchesDAL);

            // רשימה של אובייקטים מסוג מחלקה שיצרנו- list matches ל matchesDAL list  המרת 
            //המתאימה לנתונים שהתאמה אמורה להחזיר
            matchesDAL.ForEach(d => matches.Add(new ClassForMatches() {
                ProductId=d.ProductId,
                ProductDescription=d.ProductDescription,
                ProductName=d.ProductName,
                AddressPointX=d.AddressPointX,
                AddressPointY=d.AddressPointY,
                AddressDescription=d.AddressDescription,
                UserName=d.User.UserFullName,
                CategoryName=d.Category.CategoryName,
                DateFound=d.DateFound,
                LostOrFound=d.LostOrFound,
                UserEmail=d.User.UserEmail,
                UserPhone=d.User.UserPhone
            }));
            return matches;
        }

        //פונקציה שעוברת על ההתאמות החדשות ובודקת על כל התאמה האם מוגדר סוכן חכם על הפריט 
        //'ואם כן שולחת מייל לבעל החפץ עם ההודעה 'נמצאה התאמה חדשה למוצרך
        private static void checkIfSendEmails(List<DAL.Product> matches)
        {
            matches.ForEach(m =>
            {
                if(m.CleverAgent==true)
                {
                    string text; string subject; string mailToSend;
                    if (m.LostOrFound == true)
                        subject = " 'EasyToFind'  מצא התאמה חדשה לאבדתך";
                    else subject = " 'EasyToFind'  מצא התאמה חדשה למציאתך";
                    text = "  'EasyToFind' לצפייה בהתאמה כנס לאתר";
                    if(m.User.UserEmail!=null)
                    {
                        mailToSend = m.User.UserEmail;
                        SendEmail.SendEmail1(text, subject, mailToSend);
                    }
                }
            });
        }
        //פונקציה שמקבלת שתי מוצרים ובודקת האם ערכי הפרמטרים שלהם זהים 
        //פעמיים ומוצאת לכל מוצר את הפרמטרים parameterOfProduct כלומר עוברת בלולאה על טבלת 
        // השייכים אליו ויוצרת מהם רשימה
        //שווה value of the ParameterOfProduct,אח"כ משווה בין שתי הרשימות ובודקת האם ה
        private static bool checkTheParameters(int productId1, int productId2)
        {
            List<DAL.ParameteOfProduct> p1 = db.ParameteOfProducts.Where(p => p.ProductId == productId1).ToList();
            List<DAL.ParameteOfProduct> p2 = db.ParameteOfProducts.Where(p => p.ProductId == productId2).ToList();
            DAL.ParameteOfProduct p3;
            for (int i = 0; i < p1.Count(); i++)
            {
                p3 = p2.FirstOrDefault(p => p.ParameterId == p1[i].ParameterId);
                if (p3 != null)
                    if (!p3.Value.Contains(p1[i].Value) && !p1[i].Value.Contains(p3.Value))
                        return false;
                p3 = null;
            }
            return true;
        }
        //פונקציה שמקבלת שתי נקודות על המפה ומחזירה מרחק
        public static GeographicData GetDistanceAndDuration(double? fromX, double? fromY, double? toX, double? toY)
        {
            var gr = new GeographicData();
            try
            {
                var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={fromX},{fromY}&destinations={toX},{toY}&key=AIzaSyB6XGmiIhsaoXzLTu611HLGNL74ZEWIaSE&language=he";
                var request = WebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var res = sr.ReadToEnd();
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    dynamic result = json_serializer.DeserializeObject(res);
                    var details = result["rows"][0]["elements"][0];

                    gr.Distance = details["distance"]["text"];
                    gr.Duration = details["duration"]["text"];
                }
            }
            catch (Exception) { }
            return gr;
        }
    }
}