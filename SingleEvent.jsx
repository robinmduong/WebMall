import React from "react";
import logger from "sabio-debug";
import PropTypes from "prop-types";
import { withRouter } from "react-router-dom";
import "./UserDash.scss";
import moment from "moment";

const _logger = logger.extend("SingleEvent");

const SingleEvent = (props) => {
  _logger(props);

  return (
    <React.Fragment>
      <div className="user-dash-shopping-cart-card user-dash-card shadow">
        <div className="user-dash-card-body">
          <div className="table-responsive text-center user-status">
            <table className="table table-borderless">
              <thead>
                <tr></tr>
              </thead>
              <tbody>
                <tr>
                  <th className="bd-t-none" scope="row">
                    <img
                      src={props.event.imageUrl}
                      alt="Event"
                      className="user-dash-icon"
                    />
                  </th>
                  <td>
                    <h5>{props.event.name}</h5>
                  </td>
                  <td>
                    <div className="event-start-date">
                      <i className="fa fa-clock-o"></i>
                      <small> Start Date:</small>
                      <h6>
                        {" "}
                        {moment(props.event.dateStart).format(
                          "dd. MM/DD/YY, hh:mm a"
                        )}
                      </h6>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </React.Fragment>
  );
};

SingleEvent.propTypes = {
  event: PropTypes.shape({
    imageUrl: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    dateStart: PropTypes.oneOfType([
      PropTypes.instanceOf(Date).isRequired,
      PropTypes.string.isRequired,
    ]),
    dateEnd: PropTypes.oneOfType([
      PropTypes.instanceOf(Date).isRequired,
      PropTypes.string.isRequired,
    ]),
    event: PropTypes.oneOfType([
      PropTypes.bool.isRequired,
      PropTypes.string.isRequired,
    ]),
  }),
};

export default withRouter(SingleEvent);
