import { useEffect, useState } from "react";
import sellerService from "../../../services/sellerService";
import ProductsR from "../../../reusable/Product/ProductsR";

const Products = () => {
  const [products, setProducts] = useState([]);
  const updateProducts = () => sellerService.getProducts().then((res) => setProducts(res));

  useEffect(() => {
    updateProducts();
  }, []);

  return (
    <ProductsR products={products} updateProducts={updateProducts} title={"My products"}/>
  );
};

export default Products;
