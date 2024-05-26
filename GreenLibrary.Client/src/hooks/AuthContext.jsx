import React, { createContext, useContext, useState, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';

const AuthContext = createContext(null);


//AuthProvider holds authentication state and provides login and logout
export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(() => {
        const token = localStorage.getItem('token');
        if (token) {
            const decodedToken = jwtDecode(token);
            if (new Date().getTime() < decodedToken.exp * 1000) {
                const id = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
                const roles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
                return { token, roles, id, ...decodedToken };
            }
        }
        return null;
    });

    useEffect(() => {
        const interval = setInterval(() => {
            const token = localStorage.getItem('token');
            if (token) {
                const decodedToken = jwtDecode(token);
                if (new Date().getTime() >= decodedToken.exp * 1000) {
                    logout();
                }
            } else {
                logout();
            }
        }, 1000 * 60); // Check every minute

        return () => clearInterval(interval);
    }, []);

    const login = (token) => {
        const decodedToken = jwtDecode(token);
        const roles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        localStorage.setItem('token', token);
        setUser({ token, roles, ...decodedToken });
    };

    const logout = () => {
        localStorage.removeItem('token');
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

