import React, { createContext, useContext, useState, useEffect } from 'react';


const AuthContext = createContext(null);


//AuthProvider holds authentication state and provides login and logout
export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(() => {
        const token = localStorage.getItem('token');
        const username = localStorage.getItem('username');
        const roles = localStorage.getItem('roles');
        const expiresAt = localStorage.getItem('expires_at');
        if (token && new Date().getTime() < expiresAt) {
            return { token, username, roles };
        }
        return null;
    });

    useEffect(() => {
        const interval = setInterval(() => {
            const token = localStorage.getItem('token');
            const expiresAt = localStorage.getItem('expires_at');
            if (!token || new Date().getTime() >= expiresAt) {
                logout();
            }
        }, 1000 * 60); // Check every minute

        return () => clearInterval(interval);
    }, []);

    const login = (token, username, roles, expiresIn) => {
        const expiresAt = new Date().getTime() + expiresIn * 1000; // Convert expiresIn to milliseconds
        localStorage.setItem('token', token);
        localStorage.setItem('username', username);
        localStorage.setItem('roles', roles);
        localStorage.setItem('expires_at', expiresAt);
        setUser({ token, username, roles });
    };

    const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('roles');
        localStorage.removeItem('expires_at');
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

