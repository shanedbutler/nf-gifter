import React from "react";
import { Route, Routes } from "react-router-dom";
import "./App.css";
import ApplicationViews from "./components/ApplicationViews";
import AuthNav from "./components/AuthNav";
import { Authorized } from "./components/Authorized";
import Header from "./components/Header";
import { Login } from "./components/Login";
import { Register } from "./components/Register";

function App() {

  return (
    <div className="App">
      <Routes>
        <Route path="/login" element={
          <>
            <AuthNav />
            <Login />
          </>
        } />
        <Route path="/register" element={
          <>
            <AuthNav />
            <Register />
          </>
        } />
        <Route path="*" element={
          <Authorized>
            <Header />
            <ApplicationViews />
          </Authorized>
        } />
      </Routes>
    </div>
  );
}

export default App;
