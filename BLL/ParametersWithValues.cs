﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BLL
{
    public class ParametersWithValues
        //parameterId,parameterName,value,categoryId פונקציה שמחזירה את כל הפרמטרים הקשורים למוצר כולל 
    {
        public static List<customClasses.ParametersWithParametersOfProducts> getParametersWithValue(int productID)
        {
            List<Parameter> parametersByCategory;
            List<customClasses.ParametersWithParametersOfProducts> parametersWithValue = new List<customClasses.ParametersWithParametersOfProducts>();;
            int categotyID;

            using (AshavatAvedaEntities db = new AshavatAvedaEntities())
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