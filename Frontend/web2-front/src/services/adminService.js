import api from '../api/api'
import { OrderModel, UserModel } from '../models/models';

const getVerifiedUsers = async() => {
    try {
        const res = await api.get('admin/verified-users');
        return res.data ? res.data.map(o => new UserModel(o)) : [];
    }
    catch(e) {
        alert(e.response.data.Exception);
        return [];
    }
}

const getWaitingUsers = async() => {
    try {
        const res = await api.get('admin/waiting-users');
        return res.data ? res.data.map(o => new UserModel(o)) : [];
    }
    catch(e) {
        alert(e.response.data.Exception);
        return [];
    }
}

const getDeclinedUsers = async() => {
    try {
        const res = await api.get('admin/declined-users');
        return res.data ? res.data.map(o => new UserModel(o)) : [];
    }
    catch(e) {
        alert(e.response.data.Exception);
        return [];
    }
}

const getBuyers = async() => {
    try {
        const res = await api.get('admin/buyers');
        return res.data ? res.data.map(o => new UserModel(o)) : [];
    }
    catch(e) {
        alert(e.response.data.Exception);
        return [];
    }
}

const getOrders = async() => {
    try {
        const res = await api.get('admin/orders');
        return res.data ? res.data.map(o => new OrderModel(o)) : [];
    }
    catch(e) {
        alert(e.response.data.Exception);
        return [];
    }
}

const postVerifyUser = async(data) => {
    try {
        await api.post('admin/verify-user', data);
        return true;
    }
    catch(e) {
        alert(e.response.data.Exception);
        return false;
    }
}

// eslint-disable-next-line import/no-anonymous-default-export
export default {
    getVerifiedUsers,
    getWaitingUsers,
    getOrders,
    postVerifyUser,
    getBuyers,
    getDeclinedUsers
}