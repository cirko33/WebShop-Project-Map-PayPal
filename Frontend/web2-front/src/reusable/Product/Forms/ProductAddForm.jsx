import {
  Button,
  Card,
  CardActionArea,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TextField,
  Typography,
} from "@mui/material";

import classes from "./Forms.module.css";
import { useState } from "react";
import sellerService from "../../../services/sellerService";

const ProductAddForm = ({ updateProducts }) => {
  const [data, setData] = useState({
    name: "",
    description: "",
    amount: 0,
    price: 0,
    imageFile: "",
  });
  const [open, setOpen] = useState(false);

  const handleClose = (e) => setOpen(false);
  const handleSave = (e) => {
    e.preventDefault();
    if (!data.name || !data.price || !data.description || !data.amount) {
      alert("All fields are required");
      return;
    }

    if (!data.amount || data.amount < 0 || !parseInt(data.amount)) {
      alert("Amount is integer and must be over 0.");
      return;
    }

    if (!data.price || data.price < 0 || !parseFloat(data.price)) {
      alert("Price is floater and must be over 0.");
      return;
    }

    const formData = new FormData();
    for (const prop in data) {
      formData.append(prop, data[prop]);
    }
    sellerService.postProduct(formData).then((res) => res && updateProducts());
    setOpen(false);
  };
  const handleChange = (e) => {
    setData({
      ...data,
      [e.target.id]: e.target.value,
    });
  };

  const handleChangeNumber = (e) => {
    let value = "";
    if (e.target.value) {
      value = e.target.value > 0 ? e.target.value : 0;
    }

    setData({
      ...data,
      [e.target.id]: value,
    });
  };
  return (
    <>
      <Dialog open={open} onClose={handleClose} sx={{ color: "white", backgroundColor: "#0c1215" }}>
        <DialogTitle>Add product</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="name"
            label="Name"
            type="text"
            fullWidth
            variant="standard"
            value={data.name}
            onChange={handleChange}
            required
          />
          <TextField
            autoFocus
            margin="dense"
            id="price"
            label="Price"
            type="number"
            fullWidth
            variant="standard"
            value={data.price}
            onChange={handleChangeNumber}
            required
          />
          <TextField
            autoFocus
            margin="dense"
            id="amount"
            label="Amount"
            type="number"
            fullWidth
            variant="standard"
            value={data.amount}
            onChange={handleChangeNumber}
            required
          />
          <TextField
            autoFocus
            margin="dense"
            id="description"
            label="Description"
            type="text"
            fullWidth
            variant="standard"
            value={data.description}
            onChange={handleChange}
            required
          />
          <img
            title="Image"
            alt="Add"
            src={data.imageFile && URL.createObjectURL(data.imageFile)}
            className={classes.image}
          />
          <div>
            <input
              id="imageFile"
              label="Image"
              type="file"
              accept="image/jpg"
              onChange={(e) => {
                setData({ ...data, imageFile: e.target.files[0] });
              }}
            />
          </div>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSave}>Save</Button>
        </DialogActions>
      </Dialog>
      <Card
        className={classes.card}
        sx={{ color: "white", background: "#0c1215", display: "flex", alignItems: "center", justifyItems: "center" }}
      >
        <CardActionArea sx={{ display: "flex", height: "100%" }} onClick={(e) => setOpen(true)}>
          <Typography sx={{ fontSize: 120 }}>+</Typography>
        </CardActionArea>
      </Card>
    </>
  );
};

export default ProductAddForm;
