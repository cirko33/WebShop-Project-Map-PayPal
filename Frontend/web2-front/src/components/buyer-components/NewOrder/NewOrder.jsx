import { useContext, useEffect, useState } from "react";
import buyerService from "../../../services/buyerService";
import { CartContext } from "../../../contexts/cart-context";
import ProductsR from "../../../reusable/Product/ProductsR";

const NewOrder = () => {
  const [products, setProducts] = useState([]);
  const updateProducts = () =>
    buyerService.getProducts().then((res) => {
      setProducts(res);
      const temp = {...cart};
      for (const i in res) {
        if (!temp[res[i].id]) 
          temp[res[i].id] = 0;
      }
      setCart(temp);
    });
  const { cart, setCart } = useContext(CartContext);

  useEffect(() => {
    updateProducts();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <ProductsR products={products} updateProducts={updateProducts} title={"Products"}/>
  );
};

export default NewOrder;
