using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace bllCharity
{
    public class ItemImage : ErrorManager
    {
        public ItemImage(int itemId, int userId, string description, byte[] data)
        {
            ItemId = itemId;
            UserId = userId;
            Description = description;
            Data = data;
        }

        public ItemImage(DataRow dr)
        {
            Id = (int)dr["Id"];
            ItemId = (int)dr["ItemId"];
            UserId = (int)dr["UserId"];
            SequenceNo = (int)dr["SequenceNo"];
            Description = (string)dr["Description"];
            Data = (byte[])dr["Image"];
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public int SequenceNo { get; set; }
        public string Description { get; set; }
        public byte[] Data { get; set; }

        public string Filename
        {
            get
            {
                return "Image" + Id + ".jpg";
            }
        }

        public string Path
        {
            get
            {
                return System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/images/cache"), Filename);
            }
        }

        public bool Create()
        {
            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@itemId", ItemId);
            parameters.Add("@userId", UserId);
            parameters.Add("@sequenceNo", SequenceNo);
            parameters.Add("@description", Description);
            parameters.Add("@image", Data);

            Database db = Database.Instance;
            object obj = db.Scalar("sp_ItemImages_Create", parameters);
            if (obj == null)
            {
                this.Exception = db.Exception;
            }
            else
            {
                int id = (int)obj;
                if (id > 0)
                {
                    Id = id;
                    return true;
                }
            }
            return false;
        }
    }

    public class ItemImages : List<ItemImage>
    {
        public ItemImages()
        {
        }

        public ItemImages(int itemId) : this()
        {
            ItemId = itemId;
        }

        public int ItemId { get; set; }

        public static ItemImages SelectByItemId(int itemId)
        {
            ItemImages images = new ItemImages(itemId);

            DatabaseParameters parameters = new DatabaseParameters();
            parameters.Add("@itemId", itemId);

            DataTable dt = Database.Instance.Query("sp_ItemImages_Get", parameters);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ItemImage image = new ItemImage(dr);
                    images.Add(image);
                    if (!System.IO.File.Exists(image.Path))
                    {
                        FileStream fs = new FileStream(image.Path, FileMode.CreateNew, FileAccess.Write);
                        try
                        {
                            fs.Write(image.Data, 0, image.Data.Length);
                        }
                        finally
                        {
                            fs.Close();
                        }
                    }
                }
            }
            return images;
        }

        public bool AddImage(int userId, string description, byte[] data)
        {
            ItemImage image = new ItemImage(ItemId, userId, description, data);
            if (image.Create())
            {
                this.Add(image);
                return true;
            }
            return false;
        }
    }
}