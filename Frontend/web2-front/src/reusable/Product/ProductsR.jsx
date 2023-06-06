import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import { useContext, useState } from "react";
import sellerService from "../../services/sellerService";
import classes from "./ProductsR.module.css";
import ProductUpdateForm from "./Forms/ProductUpdateForm";
import ProductAddForm from "./Forms/ProductAddForm";
import { convertImage } from "../../helpers/helpers";
import AuthContext from "../../contexts/auth-context";
import { CartContext } from "../../contexts/cart-context";
import ConfirmDialog from "./Forms/ConfirmDialog";

const ProductsR = ({ products, updateProducts, title }) => {
  const [open, setOpen] = useState(false);
  const [data, setData] = useState({});
  const context = useContext(AuthContext);
  const { cart, setCart } = useContext(CartContext);

  const changeValue = (id, value, maxAmount) => {
    setCart({ ...cart, [id]: value < 0 ? 0 : Math.min(maxAmount, value) });
  };

  const cartNotEmpty = () => {
    for (const i in cart) {
      if (cart[i] > 0) return true;
    }
    return false;
  };

  return (
    <div>
      <Typography variant="h4" sx={{ display: "flex", justifyContent: "center", color: "blue" }}>
        {title}
      </Typography>
      <div className={classes.cardContainer}>
        {context.inType("Seller") && (
          <>
            <ProductUpdateForm
              open={open}
              setOpen={setOpen}
              data={data}
              setData={setData}
              updateProducts={updateProducts}
            />
            <ProductAddForm updateProducts={updateProducts} />
          </>
        )}

        {context.inType("Buyer") && <ConfirmDialog open={open} setOpen={setOpen} products={products} />}

        {products &&
          products.length > 0 &&
          products.map((p, index) => (
            <Card className={classes.card} sx={{ color: "white", background: "#0c1215" }} key={index}>
              <CardMedia
                component="img"
                alt="No pic"
                sx={{ height: 150, width: "100%", objectFit: "contain" }}
                image={p.image && convertImage(p.image)}
              />
              <CardContent>
              {context.inType("Seller") && <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Product ID: {p.id}</Typography> }
              {context.inType("Buyer") && <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Seller: {p.seller.name}</Typography>}
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Name: {p.name}</Typography>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Price: {p.price}</Typography>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Amount: {p.amount}</Typography>
                <Typography sx={{ fontSize: 14, flexWrap: "wrap" }}>Description: {p.description}</Typography>
              </CardContent>
              <CardActions>
                {context.inType("Seller") && (
                  <>
                    <Button
                      size="small"
                      sx={{ fontWeight: "bold" }}
                      color="success"
                      onClick={(e) => {
                        setData({ ...p, imageFile: "" });
                        setOpen(true);
                      }}
                    >
                      Edit
                    </Button>
                    <Button
                      size="small"
                      sx={{ fontWeight: "bold" }}
                      color="error"
                      onClick={(e) => sellerService.deleteProduct(p.id).then((res) => res && updateProducts())}
                    >
                      Delete
                    </Button>
                  </>
                )}

                {context.inType("Buyer") && (
                  <>
                    <Button
                      variant="contained"
                      sx={{
                        minWidth: "20px",
                        minHeight: "20px",
                        maxWidth: "20px",
                        maxHeight: "20px",
                        marginRight: "10px",
                        marginLeft: "10px",
                      }}
                      onClick={(e) => changeValue(p.id, cart[p.id] - 1, p.amount)}
                    >
                      {"<"}
                    </Button>
                    <input
                      className={classes.numb}
                      pattern="[0-9]{0,4}"
                      placeholder="0"
                      value={cart[p.id]}
                      onChange={(e) => changeValue(p.id, e.target.value, p.amount)}
                    />
                    <Button
                      sx={{
                        minWidth: "20px",
                        minHeight: "20px",
                        maxWidth: "20px",
                        maxHeight: "20px",
                        marginRight: "10px",
                        marginLeft: "10px",
                      }}
                      variant="contained"
                      onClick={(e) => changeValue(p.id, cart[p.id] + 1, p.amount)}
                    >
                      {">"}
                    </Button>
                  </>
                )}
              </CardActions>
            </Card>
          ))}
      </div>
      {context.inType("Buyer") && cartNotEmpty() && (
        <Button
          sx={{
            position: "fixed",
            bottom: 0,
            right: 0,
            zIndex: 9999,
            minWidth: "50px",
            minHeight: "50px",
            maxWidth: "50px",
            maxHeight: "50px",
            marginRight: "50px",
            marginBottom: "50px",
          }}
          variant="contained"
          onClick={(e) => setOpen(true)}
        >
          Buy
        </Button>
      )}
    </div>
  );
};

export default ProductsR;
