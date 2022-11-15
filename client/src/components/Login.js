import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../modules/UserManager";

export const Login = () => {

    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const submitLoginForm = (e) => {
        const newLogin = {
            email: email,
            password: password
        }
        e.preventDefault();
        login(newLogin);
        navigate("/");
    };

    return (
        <>
            <h2>Log In</h2>
            <form>
                <div className="form-group">
                    <label htmlFor="exampleInputEmail1">Email address: </label>
                    <input
                        type="text"
                        onChange={(e) => setEmail(e.target.value)}
                        name="email"
                    />
                    <label htmlFor="exampleInputEmail1">Password: </label>
                    <input
                        type="password"
                        onChange={(e) => setPassword(e.target.value)}
                        name="password"
                    />
                </div>
                <button type="submit" onClick={submitLoginForm}>
                    Log In
                </button>
            </form>
        </>
    );
};
