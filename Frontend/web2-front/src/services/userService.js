import api from "../api/api"
import { UserModel } from "../models/models";


const register = async (data) => {
    try {
        await api.post("auth/register", data, { headers: { "Content-Type":"multipart/form-data" }});
        return true;
    }
    catch(e) {
        alert(e.response.data.Exception);
        return false;
    }
}

const getUser = async () => {
    try {
        const res = await api.get("profile");
        return res.data ? new UserModel(res.data) : null;
    }
    catch(e) {
        alert(e.response.data.Exception);
        return null;
    }
}

const setUser = async (data) => {
    try {
        await api.put("profile", data, { headers: { "Content-Type":"multipart/form-data" }});
        return true;
    }
    catch(e) {
        alert(e.response.data.Exception);
        return false;
    }
}


// eslint-disable-next-line import/no-anonymous-default-export
export default {
    register,
    getUser,
    setUser,
}