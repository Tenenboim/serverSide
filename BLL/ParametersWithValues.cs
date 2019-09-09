using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
    public class ParametersWithValues
    {
        /// <summary>
        /// 
        /// מסוג מחלקה שיצרתי בעצמי list פונקציה המקבלת קוד מוצר ומחזירה 
        /// מחלקה המכילה קוד קטגוריה, קוד פרמטר שם פרמטר (ממחלקת פרמטר) וערך הפרמטר 
        ///parameterOfProduct ממחלקת 
        ///פונקציה זו נועדה בשביל עריכת מוצר 
        ///כאשר אני מציגה מוצר למטרת עריכה אני רוצה להציג גם את
        ///parametrOfProduct הפרמטרים הקשורים למוצר זה (מחלקת פרמטר) וכן את ערכי הפרמטרים שנבחרו (מחלקת 
        ///לכן בגלל שהערכים נמצאים בשתי טבלאות יצרתי מחלקה חדשה עם הערכים הנצרכים לי להצגה
        ///וכך הפוקציה מחזירה את כל הפרמרים עם הערכים הקשורים למוצאר שקיבלה
        /// </summary>
        /// <param name="productID"></param>
        /// <returns>customClasses.ParametersWithParametersOfProducts</returns>
        public static List<customClasses.ParametersWithParametersOfProducts> 
            getParametersWithValue(int productID)
        {
            List<Parameter> parametersByCategory;
            List<customClasses.ParametersWithParametersOfProducts> parametersWithValue 
                = new List<customClasses.ParametersWithParametersOfProducts>();
            int categotyID;

            using (Entities db = new Entities())
            {
                Product product = db.Products.Find(productID);

                if (product != null)
                {
                    categotyID = product.CategoryId;
                    //item.CategoryId==קוד הקטגוריה של הקטגוריה ששמה- כל הקטגוריות
                    parametersByCategory = db.Parameters.Where(item => item.CategoryId == categotyID||item.CategoryId==1).ToList();
                    parametersByCategory.ForEach(item =>
                    {
                        parametersWithValue.Add(new customClasses.ParametersWithParametersOfProducts { CategoryId = categotyID, ParameterId = item.ParameterId, ParameterName = item.ParameterName, ParameterOfProductValue = item.ParameteOfProducts.Count != 0 ? item.ParameteOfProducts.FirstOrDefault().Value : null });
                    });

                    return parametersWithValue.ToList();

                }

                else
                    return null;
            }

        }
    }
}
