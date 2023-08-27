﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Validator.Products;

namespace Telegram.BOT.Domain.Products
{
    public class Product : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string Tags { get; private set; }
        public DateTime CreateDate { get; private set; }
        public List<Groups> Group75 { get; set; }
        public List<Groups> Group50 { get; set; }
        public List<Groups> Group25 { get; set; }

        public Product(Guid id, string name, string description, string image, string tags, DateTime createDate)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Tags = tags;
            CreateDate = createDate;
            Validate(this, new ProductValidator());
        }
    }
}