using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListTot : MonoBehaviour
{
    [Serializable]
    public class Recipe
    {
        public ItemListTot.Item _resultItem;
        public int _resultCount;

        public ItemListTot.Item _mat1;
        public int _mat1Count;

        public ItemListTot.Item _mat2;
        public int _mat2Count;

        public ItemListTot.Item _mat3;
        public int _mat3Count;

        public Recipe(ItemListTot.Item resultItem, int resultCount, ItemListTot.Item mat1, int mat1Count, ItemListTot.Item mat2, int mat2Count, ItemListTot.Item mat3, int mat3Count)
        {
            _resultItem = resultItem;
            _resultCount = resultCount;
            _mat1 = mat1;
            _mat1Count = mat1Count;
            _mat2 = mat2;
            _mat2Count = mat2Count;
            _mat3 = mat3;
            _mat3Count = mat3Count;
        }
    }

    public List<Recipe> _recipeListTot = new List<Recipe>();

}
