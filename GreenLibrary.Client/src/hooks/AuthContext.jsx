import React, { useState, createContext, useContext } from 'react';


const AuthContext = createContext(null);


//AuthProvider holds authentication state and provides login and logout
export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [role, setRole] = useState(null);

    const login = (userData) => {
        localStorage.setItem('user', JSON.stringify(userData));
        setUser(userData);
    };

    const logout = () => {
        localStorage.removeItem('user');
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, role, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};


//UseAuth get the user state (is it logout or login)
export const useAuth = () => useContext(AuthContext);

