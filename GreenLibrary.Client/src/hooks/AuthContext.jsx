import React, { createContext, useContext, useState, useEffect } from 'react';


const AuthContext = createContext(null);


//AuthProvider holds authentication state and provides login and logout
export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(() => {
        const token = localStorage.getItem('token');
        const username = localStorage.getItem('username');
        const roles = localStorage.getItem('roles');
        return token ? { token, username, roles } : null;
    });

    const login = (token, username, roles) => {
        localStorage.setItem('token', token);
        localStorage.setItem('username', username);
        localStorage.setItem('roles', roles);
        setUser({ token, username, roles });
    };

    const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('roles');
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};


//UseAuth get the user state (is it logout or login)
export const useAuth = () => useContext(AuthContext);

