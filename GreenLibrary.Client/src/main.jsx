import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import 'semantic-ui-css/semantic.min.css'
import './styles.css'
import { AuthProvider } from './hooks/AuthContext';
import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:7195/api';
axios.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    config.headers.Authorization = token ? `Bearer ${token}` : '';
    return config;
});

axios.interceptors.response.use(
    response => response,
    error => {
        console.log(error);
        if (error.response && error.response.status === 401) {
            localStorage.removeItem('token');
            window.location = '/login';
        }
        return Promise.reject(error);
    }
);


ReactDOM.createRoot(document.getElementById('root')).render(
    <AuthProvider>
        <App />
    </AuthProvider>
)
