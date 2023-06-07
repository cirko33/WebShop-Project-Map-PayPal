import { createContext, useState } from "react";

export const CartContext = createContext();

export const CartContextProvider = ({ children }) => {
  const [cart, setCart] = useState();
  const [data, setData] = useState({deliveryAddress: "", comment: ""});

  return (
    <CartContext.Provider
      value={{
        cart,
        setCart,
        data, 
        setData
      }}
    >
      {children}
    </CartContext.Provider>
  );
};
