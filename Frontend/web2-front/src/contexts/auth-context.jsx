import React, { useEffect, useState } from "react";
import api from "../api/api";
import jwtDecode from "jwt-decode";
import {useNavigate} from 'react-router-dom'

const AuthContext = React.createContext();

export const AuthContextProvider = (props) => {
    const [token, setToken] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        setToken(localStorage.getItem('token'));
    }, []);

    const loginHandler = async(loginData) => {
        try {
            const res = await api.post('auth/login', loginData)
            if(!res)
                return;

            setToken(res.data);
            localStorage.setItem('token', res.data);
            navigate('/home');
        } catch (e){
            alert(e.response.data.Exception);
        }
    };

    const logoutHandler = () => {
        setToken(null);
        localStorage.clear();
        navigate("/")
    };

    const userType = () => {
        try {
            if(!token)
                return null;
            const tokenDecoded = jwtDecode(token);
            return tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        } catch(e) {
            console.log(e);
        }
    };

    const inType = (type) => {
        try {
            if(!token)
                return null;
            const tokenDecoded = jwtDecode(token);
            return tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === type;
        } catch(e) {
            console.log(e);
        }
    }

    const googleLogin = async(data) => {
        try {
            const res = await api.post('auth/google-sign-in', {token: data.credential})
            if(!res)
                return;

            setToken(res.data);
            localStorage.setItem('token', res.data);
            navigate('/home');
        } catch (e){
            alert(e.response.data.Exception);
        }
    }

    return (
        <AuthContext.Provider
        value={{
            token: token,
            onLogout: logoutHandler,
            onLogin: loginHandler,
            type: userType,
            inType: inType,
            googleLogin: googleLogin
        }}>
            
            {
                props.children
            }       
        </AuthContext.Provider>
    );
};

export default AuthContext;