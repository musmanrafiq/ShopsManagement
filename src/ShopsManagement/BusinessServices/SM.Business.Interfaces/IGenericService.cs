﻿namespace SM.Business.Interfaces
{
    public interface IGenericService<TModel>
    {
        public List<TModel> GetAll();
        public TModel GetById(int id);
        public void Add(TModel model);
        public void Update(TModel model);
        public void Delete(int id);
    }
}
