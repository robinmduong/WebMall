import React from "react";
import logger from "sabio-debug";
import PropTypes from "prop-types";
import moment from "moment";

const _logger = logger.extend("SingleAvailability");

const SingleAvailability = (props) => {
  const handleUpdate = (e) => {
    e.preventDefault();
    _logger("handleUpdate from child is running", props);
    props.update(props.availability);
  };

  const handleDelete = (e) => {
    e.preventDefault();
    _logger("handleDelete from child is running.");
    _logger(props);
    props.delete(props.availability);
  };

  return (
    <React.Fragment>
      <div className="col-sm-12 col-xl-12">
        <div className="card">
          <div className="card-header b-l-primary border-3">
            <h5>
              Scheduled Appointment{" "}
              <p>
                {moment(props.availability.startTime).format(
                  "ddd, MMM DD, YYYY - hh:mm a"
                )}
              </p>
            </h5>
          </div>
          <div className="card-body text-center">
            <div
              name="scheduleId"
              className="scheduleId d-none"
              id="scheduleId"
              alt="scheduleId"
            >
              {props.availability.scheduleId}
            </div>
            <div
              name="startTime"
              className="card-text"
              id="startTime"
              alt="startTime"
            >
              <b>Start Time: </b>
              {moment(props.availability.startTime).format(
                "ddd, MMM DD, YYYY [at] hh:mm a"
              )}
            </div>
            <div
              name="endTime"
              className="card-text"
              id="endTime"
              alt="endTime"
            >
              <b>End Time: </b>
              {moment(props.availability.endTime).format(
                " ddd, MMM DD, YYYY [at] hh:mm a"
              )}
            </div>
            <a
              href="/schedule/availability/edit"
              className="btn btn-primary update-button m-2"
              id="update-button"
              alt="update-button"
              onClick={handleUpdate}
            >
              Edit
            </a>
            <a
              href="/schedule/availability"
              className="btn btn-danger delete-button m-2"
              id="delete-button"
              alt="delete-button"
              onClick={handleDelete}
            >
              Delete
            </a>
          </div>
        </div>
      </div>
    </React.Fragment>
  );
};

SingleAvailability.propTypes = {
  update: PropTypes.func.isRequired,
  delete: PropTypes.func.isRequired,
  availability: PropTypes.shape({
    id: PropTypes.number.isRequired,
    scheduleId: PropTypes.number.isRequired,
    startTime: PropTypes.oneOfType([
      PropTypes.instanceOf(Date).isRequired,
      PropTypes.string.isRequired,
    ]),
    endTime: PropTypes.oneOfType([
      PropTypes.instanceOf(Date).isRequired,
      PropTypes.string.isRequired,
    ]),
  }),
};

export default SingleAvailability;
