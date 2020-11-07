using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO.Startup
{
    public class AttributeDAO:BaseDAO
    {
		public AttributeDAO()
		{
			db = new StartupWebsite();
		}

		public void CreateManualAttribute(long productId,int? attributeId, String stringSubAttribute, long[] listPrice)
		{
			List<String> listNameSubAttribute = stringSubAttribute.Split(',').ToList();

			foreach (string nameSubAttribute in listNameSubAttribute)
			{
				var subAttributeF = db.SubAttributes.Where(x => x.Name.ToLower() == nameSubAttribute.ToLower() && x.AttributeId == attributeId).ToList();
				AttributeRelationship newAttrRel = new AttributeRelationship();
				if (subAttributeF.Count() <= 0)
				{
					//them moi subattribute
					SubAttribute newSubAttr = new SubAttribute();
					newSubAttr.AttributeId = attributeId.Value;
					newSubAttr.Name = nameSubAttribute;
					newSubAttr.TextColor = nameSubAttribute;
					newSubAttr.BackgroundColor = nameSubAttribute;
					int? newSubAttrId = this.CreateSubAttribute(newSubAttr);

					//Tao quan he vs product
					newAttrRel.ProductId = productId;
					newAttrRel.SubAttributeId1 = newSubAttrId;
					newAttrRel.price = listPrice[listNameSubAttribute.IndexOf(nameSubAttribute)];
					createAttributeRelationship(newAttrRel);
				}
				else
				{
					newAttrRel.ProductId = productId;
					newAttrRel.SubAttributeId1 = subAttributeF[0].SubAttributeId;
					newAttrRel.price = listPrice[listNameSubAttribute.IndexOf(nameSubAttribute)];
					createAttributeRelationship(newAttrRel);
				}
			}

		}

		public void removeAttrRelProductId(long? productId)
		{
			var listAttrRel = db.AttributeRelationships.Where(x => x.ProductId == productId).ToList();
			db.AttributeRelationships.RemoveRange(listAttrRel);
		}
		public long createAttributeRelationship(AttributeRelationship newAttrRel)
		{
			db.AttributeRelationships.Add(newAttrRel);
			db.SaveChanges();
			return newAttrRel.AttributeRelationshipId;
		}
		public int? CreateSubAttribute(SubAttribute newSubAttribute)
		{
			db.SubAttributes.Add(newSubAttribute);
			db.SaveChanges();
			return newSubAttribute.SubAttributeId;
		}
		public bool DeleteAttribute(int id)
		{
			try
			{
				var attribute = db.Attributes.Find(id);
				db.Attributes.Remove(attribute);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
		public bool DeleteSubAttibute(int id)
		{
			try
			{
				var subAttribute = db.SubAttributes.Find(id);
				db.SubAttributes.Remove(subAttribute);
				db.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
	}
}