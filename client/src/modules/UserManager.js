const baseUrl = '/api';

 export const getCurrentUser = () => {
    const currentUser = localStorage.getItem("gifterUser");

    return currentUser;
  };

  export const login = (userObject) => {
    fetch(`${baseUrl}/UserProfile/GetByEmail?email=${userObject.email}`)
      .then((r) => r.json())
      .then((userObjFromDB) => {

        localStorage.setItem("gifterUser", JSON.stringify(userObjFromDB));
      })
  };

 export const register = (userObject) => {
    fetch(`${baseUrl}/userprofile`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(userObject),
    })
      .then((response) => response.json())
      .then((userObject) => {
        localStorage.setItem("gifterUser", JSON.stringify(userObject));
      });
  };

 export const logout = () => {
    localStorage.clear();
  };
