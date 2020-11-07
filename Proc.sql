USE STARTUP_WEBSITE
go

--Store PROCEDURE
CREATE PROCEDURE GetAllCustomer
AS
BEGIN
	SELECT * FROM CUSTOMER.Customer
END
GO

CREATE PROCEDURE GetCustomerByName
	@name nvarchar(200)
AS
BEGIN
	SELECT * FROM CUSTOMER.Customer WHERE Name = @name
END

GO
 
--EXECUTE dbo.GetAllCustomer

--EXECUTE dbo.GetCustomerByName @name="Nguyen Van A"

CREATE PROC GetAllProduct
AS
BEGIN
	SELECT p.*,pc.Name AS 'ProductCategoryName',s.Name AS 'Startup Name',sc.Name AS 'StartupCategoryName'
	FROM PRODUCT.Product p, STARTUP.Startup s, PRODUCT_CATEGORY.ProductCategory pc,PRODUCT_CATEGORY.StartupCategory sc
	WHERE p.StartupId = s.StartupId AND pc.ProductCategoryId = p.ProductCategoryId AND sc.StartupCategoryId = p.StartupCategoryId
END
DROP PROCEDURE dbo.GetAllProduct
GO

CREATE PROC GetSubAttributeByProdId
@ProductId BIGINT
AS
BEGIN
	SELECT attrR.AttributeRelationshipId,subAttr.Name,subAttr.TextColor, subAttr.backgroundColor,attrR.ProductId,attrR.SubAttributeId1,attrR.price 
	FROM ATTRIBUTE.AttributeRelationship attrR, ATTRIBUTE.SubAttribute subAttr 
	WHERE subAttr.SubAttributeId = attrR.SubAttributeId1 AND attrR.ProductId = @ProductId
END
DROP PROCEDURE dbo.GetAllProduct
EXECUTE dbo.GetSubAttributeByProdId @ProductId = 2
GO

CREATE FUNCTION productCountLike
(
	@productId BIGINT
)
RETURNS BIGINT
AS
BEGIN
	DECLARE @likeCountNumber BIGINT 
	SET @likeCountNumber = (
	SELECT  COUNT(pl.CustomerId) AS likeNumber
	FROM PRODUCT.Product pro, CUSTOMER_PRODUCT.ProductLiked pl
	WHERE pro.ProductId = pl.ProductId AND pro.ProductId = @productId
	GROUP BY pro.ProductId
	)
	IF(@likeCountNumber > 0)
	RETURN @likeCountNumber
	ELSE
	RETURN 0
	RETURN 0
END
GO



CREATE FUNCTION customerIsLike
(
	@productId BIGINT,
	@customerId UNIQUEIDENTIFIER
)
RETURNS BIT
AS
BEGIN
	IF(
	(SELECT COUNT(*) 
	FROM CUSTOMER_PRODUCT.ProductLiked pl 
	WHERE pl.ProductId = @productId AND pl.CustomerId = @customerId
	)>0)
	RETURN 1;
	RETURN 0;
END
GO
--DROP PROC dbo.GetProductByCustomerAndCategory
CREATE PROC	GetProductByCustomerAndCategory
@MetaTitleCategory VARCHAR(250),
@CustomerId UNIQUEIDENTIFIER 
AS
BEGIN
IF (@MetaTitleCategory = '')
SELECT (SELECT dbo.productCountLike(product.ProductId)) AS likeNumber, 
(SELECT dbo.customerIsLike(product.ProductId,@CustomerId))AS isLike, 
sw.StartupName, sw.LogoUrl,sw.WardName,product.*
	FROM PRODUCT.Product product,PRODUCT_CATEGORY.ProductCategory category,
		(SELECT s.StartupId, w.Name AS WardName, s.LogoUrl,s.Name AS StartupName 
		FROM LOCATION.ward w, STARTUP.Startup s 
		WHERE w.WardId = s.WardId) sw
	WHERE category.ProductCategoryId = product.ProductCategoryId 
	AND product.StartupId = sw.StartupId 
ELSE
SELECT (SELECT dbo.productCountLike(1)) AS likeNumber, (SELECT dbo.customerIsLike(product.ProductId,@CustomerId))AS isLike, sw.StartupName, sw.LogoUrl,sw.WardName,product.*
	FROM PRODUCT.Product product,PRODUCT_CATEGORY.ProductCategory category,
		(SELECT s.StartupId, w.Name AS WardName, s.LogoUrl,s.Name AS StartupName 
		FROM LOCATION.ward w, STARTUP.Startup s 
		WHERE w.WardId = s.WardId) sw
	WHERE category.ProductCategoryId = product.ProductCategoryId 
	AND product.StartupId = sw.StartupId AND category.MetaTitle = @MetaTitleCategory
END
GO

CREATE PROC	GetSearchProduct
@Search VARCHAR(250),
@CustomerId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT (SELECT dbo.productCountLike(product.ProductId)) AS likeNumber, 
	(SELECT dbo.customerIsLike(product.ProductId,@CustomerId))AS isLike, 
	sw.StartupName, sw.LogoUrl,sw.WardName,product.*
	FROM PRODUCT.Product product,PRODUCT_CATEGORY.ProductCategory category,
		(SELECT s.StartupId, w.Name AS WardName, s.LogoUrl,s.Name AS StartupName 
		FROM LOCATION.ward w, STARTUP.Startup s 
		WHERE w.WardId = s.WardId) sw
	WHERE category.ProductCategoryId = product.ProductCategoryId AND (product.Name LIKE '%'+@Search+'%' OR product.KeyWord LIKE '%'+@Search+'%')
	AND product.StartupId = sw.StartupId 
END
GO

CREATE PROC	GetProductDetail
@ProductId BIGINT,
@customerId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT (SELECT dbo.productCountLike(pro.ProductId)) AS likeNumber, 
		(SELECT dbo.customerIsLike(pro.ProductId,@customerId))AS isLike, 
		sw.StartupName, sw.LogoUrl,sw.WardName,pro.*
	FROM PRODUCT.Product pro,PRODUCT_CATEGORY.ProductCategory cat,
		(SELECT s.StartupId, w.Name AS WardName, s.LogoUrl,s.Name AS StartupName 
		FROM LOCATION.ward w, STARTUP.Startup s 
		WHERE w.WardId = s.WardId) sw
	WHERE cat.ProductCategoryId = pro.ProductCategoryId AND pro.ProductId = @ProductId
		AND pro.StartupId = sw.StartupId 
END
GO

CREATE PROC	GetProductByStartupCategory
@startupId BIGINT,
@startupCategoryId BIGINT NULL,
@customerId UNIQUEIDENTIFIER
AS
BEGIN
	IF @startupCategoryId IS NULL
	BEGIN
		SELECT (SELECT dbo.productCountLike(pro.ProductId)) AS likeNumber, 
			(SELECT dbo.customerIsLike(pro.ProductId,@customerId))AS isLike, 
			sw.StartupName, sw.LogoUrl,sw.WardName,pro.*
		FROM PRODUCT.Product pro,PRODUCT_CATEGORY.ProductCategory cat,
			(SELECT s.StartupId, w.Name AS WardName, s.LogoUrl,s.Name AS StartupName 
			FROM LOCATION.ward w, STARTUP.Startup s 
			WHERE w.WardId = s.WardId) sw
		WHERE cat.ProductCategoryId = pro.ProductCategoryId AND pro.StartupId = @startupId
			AND pro.StartupId = sw.StartupId 
	END
	ELSE
	BEGIN
	SELECT (SELECT dbo.productCountLike(pro.ProductId)) AS likeNumber, 
		(SELECT dbo.customerIsLike(pro.ProductId,@customerId))AS isLike, 
		sw.StartupName, sw.LogoUrl,sw.WardName,pro.*
	FROM PRODUCT.Product pro,PRODUCT_CATEGORY.ProductCategory cat,
		(SELECT s.StartupId, w.Name AS WardName, s.LogoUrl,s.Name AS StartupName 
		FROM LOCATION.ward w, STARTUP.Startup s 
		WHERE w.WardId = s.WardId) sw
	WHERE cat.ProductCategoryId = pro.ProductCategoryId AND pro.StartupId = @startupId 
	AND pro.StartupCategoryId = @startupCategoryId
		AND pro.StartupId = sw.StartupId 
	END
END
GO

GO

CREATE PROCEDURE DeleteCartByCustomerId
( @customerId UNIQUEIDENTIFIER)
As
BEGIN
	DELETE FROM CUSTOMER_PRODUCT.Cart
	WHERE CustomerId = @customerId
END
GO
--DROP PROC dbo.GetProductByStartupCategory
EXEC GetProductByStartupCategory @startupId = 1,@startupCategoryId = 4,@CustomerId = NULL
EXEC dbo.DeleteCartByCustomerId @customerId = NULL

GO
CREATE PROCEDURE GetRateByProductId
( @productId BIGINT)
As
BEGIN
	SELECT cus.CustomerId , cus.Name, cus.AvatarUrl, rat.*
	FROM PRODUCT.Product pro, [ORDER].Rate rat, [ORDER].OrderDetail orde, [ORDER].[Order] ord, CUSTOMER.Customer cus
	WHERE rat.ProductId = pro.ProductId AND rat.OrderDetailId = orde.OrderDetailId 
	AND orde.OrderId = ord.OrderId AND ord.CustomerId = cus.CustomerId
	AND pro.ProductId = @productId
END
GO

EXEC dbo.GetRateByProductId @productId = 2 -- bigint



