import axios from "axios";
import logger from "sabio-debug";
import * as serviceHelpers from "../services/serviceHelpers";

const _logger = logger.extend("availabilityService");

const availabilityService = `${serviceHelpers.API_HOST_PREFIX}/api/availability/`;

let getByCreatedBy = (pageIndex, pageSize) => {
  _logger("getByCreatedBy is running.");
  const config = {
    method: "GET",
    url: `${availabilityService}current?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    crossDomain: true,
    withCredentials: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config)
    .then(serviceHelpers.onGlobalSuccess)
    .catch(serviceHelpers.onGlobalError);
};

let deleteById = (id) => {
  _logger("Delete is running.");
  const config = {
    method: "DELETE",
    url: `${availabilityService}${id}`,
    crossDomain: true,
    withCredentials: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config)
    .then(serviceHelpers.onGlobalSuccess)
    .catch(serviceHelpers.onGlobalError);
};

let getAll = () => {
  _logger("getAll is running.");
  const config = {
    method: "GET",
    url: `${availabilityService}`,
    crossDomain: true,
    withCredentials: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config)
    .then(serviceHelpers.onGlobalSuccess)
    .catch(serviceHelpers.onGlobalError);
};

let get = (id) => {
  _logger("Get is running.");
  const config = {
    method: "GET",
    url: `${availabilityService}${id}`,
    crossDomain: true,
    withCredentials: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config)
    .then(serviceHelpers.onGlobalSuccess)
    .catch(serviceHelpers.onGlobalError);
};

let paginate = (pageIndex, pageSize) => {
  _logger("Paginate is running");
  const config = {
    method: "GET",
    url: `${availabilityService}paginate?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    crossDomain: true,
    withCredentials: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config)
    .then(serviceHelpers.onGlobalSuccess)
    .catch(serviceHelpers.onGlobalError);
};

let update = (id, payload) => {
  _logger("Update is running.");
  const config = {
    method: "PUT",
    url: `${availabilityService}${id}`,
    crossDomain: true,
    withCredentials: true,
    data: payload,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config)
    .then(serviceHelpers.onGlobalSuccess)
    .catch(serviceHelpers.onGlobalError);
};

let add = (newAvailability) => {
  _logger("Add is running.");
  const config = {
    method: "POST",
    url: `${availabilityService}`,
    crossDomain: true,
    withCredentials: true,
    data: newAvailability,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config)
    .then(serviceHelpers.onGlobalSuccess)
    .catch(serviceHelpers.onGlobalError);
};

export { getByCreatedBy, deleteById, getAll, get, paginate, update, add };
