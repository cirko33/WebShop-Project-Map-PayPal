import { Navigate, Route, Routes } from "react-router-dom";
import Login from "../components/Login/Login";
import Register from "../components/Register/Register";
import Dashboard from "../components/Dashboard/Dashboard";
import AuthContext from "../contexts/auth-context";
import { useContext } from "react";
import Profile from "../components/Profile/Profile";
import Verifications from "../components/admins-components/Verifications/Verifications";
import AllOrders from "../components/admins-components/AllOrders/AllOrders";
import NewOrders from "../components/sellers-components/NewOrders/NewOrders";
import MyOrders from "../components/sellers-components/MyOrders/MyOrders";
import Products from "../components/sellers-components/Products/Products";
import PreviousOrders from "../components/buyer-components/PreviousOrders/PreviousOrders";
import NewOrder from "../components/buyer-components/NewOrder/NewOrder";

const Router = () => {
    const context = useContext(AuthContext);
    
    return (  
        <Routes>
            <Route path="/" element={context.token ? <Navigate to="/home"/> : <Login />} />
            <Route path="/register" element={context.token ? <Navigate to="/home"/> : <Register />}/>
            <Route path="/home" element={context.token ? <Dashboard /> : <Navigate to="/"/>} />
            <Route path="/profile" element={context.token ? <Profile /> : <Navigate to="/"/>} />
            {/* Admin */}
            <Route path="/verifications" element={context.token && context.type() === "Administrator" ? <Verifications /> : <Navigate to="/"/>} />
            <Route path="/all-orders" element={context.token && context.type() === "Administrator" ? <AllOrders /> : <Navigate to="/"/>} />
            {/* Seller */}
            <Route path="/new-orders" element={context.token && context.type() === "Seller" ? <NewOrders /> : <Navigate to="/"/>} />
            <Route path="/my-orders" element={context.token && context.type() === "Seller" ? <MyOrders /> : <Navigate to="/"/>} />
            <Route path="/products" element={context.token && context.type() === "Seller" ? <Products /> : <Navigate to="/"/>} />
            <Route path="/new-order" element={context.token && context.type() === "Buyer" ? <NewOrder /> : <Navigate to="/"/>} />
            <Route path="/previous-orders" element={context.token && context.type() === "Buyer" ? <PreviousOrders /> : <Navigate to="/"/>} />
        </Routes>
    );
}
 
export default Router;