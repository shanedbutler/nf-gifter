const baseUrl = '/api/post';

export const getAllPosts = () => {
  return fetch(`${baseUrl}/GetWithComments`)
    .then((res) => res.json())
};

// api/Post/search?q=<query>
export const getBySearch = (query) => {
  return fetch(`${baseUrl}/search/?q=${query}`)
  .then((res) => res.json())
};

// api/Post/GetWithComments/<id>
export const getPost = (id) => {
  return fetch(`${baseUrl}/GetWithComments/${id}`).then((res) => res.json());
};

export const addPost = (singlePost) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(singlePost),
  });
};
