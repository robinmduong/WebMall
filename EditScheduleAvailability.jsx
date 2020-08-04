import React, { Component } from "react";
import { FormGroup, Label, Button } from "reactstrap";
import { Formik, Field, Form } from "formik";
import logger from "sabio-debug";
import * as availabilityService from "../../services/ScheduleAvailabilityService";
import dateValidationSchema from "../../schemas/dateValidationSchema";
import PropTypes from "prop-types";
import { toast } from "react-toastify";

const _logger = logger.extend("EditScheduleAvailability");

class EditScheduleAvailability extends Component {
  constructor(props) {
    super(props);

    this.state = {
      formData: {
        scheduleId: 69, //same as userId (is in URL for an update) - this must be a valid Id number in the schedule table
        startTime: "",
        endTime: "",
      },
    };
  }

  componentDidMount() {
    _logger("componentDidMount is running.");
    const { id } = this.props.match.params;
    if (id) {
      const { state } = this.props.location;
      if (state) {
        this.setForm(state);
      }
    }
  }

  setForm = (formInfo) => {
    _logger("setForm is running.");
    this.setState((prevState) => {
      return {
        ...prevState,
        formData: {
          ...formInfo,
        },
      };
    });
  };

  handleSubmit = (values) => {
    _logger("handleSubmit is running.");
    const id = values.id;
    if (id) {
      _logger(id);
      this.onUpdateAvailability(values);
    } else {
      this.onAddAvailability(values);
    }
  };

  onUpdateAvailability = (values) => {
    _logger("onUpdateAvailability is running.");
    availabilityService
      .update(values.id, values)
      .then(this.onUpdateSuccess)
      .catch(this.onUpdateError);
  };

  onUpdateSuccess = (response) => {
    toast.success("Schedule availability updated!");
    _logger(response);
    this.props.history.push("/schedule/availability");
  };

  onUpdateError = (error) => {
    toast.error("Schedule availability failed to update.");
    _logger(error);
  };

  onAddAvailability = (values) => {
    _logger("onAddAvailability is running.");
    availabilityService
      .add(values)
      .then(this.onAddSuccess)
      .catch(this.onAddError);
  };

  onAddSuccess = (response) => {
    toast.success("New schedule availability added!");
    _logger(response);
    this.props.history.push("/schedule/availability");
  };

  onAddError = (error) => {
    toast.error("Schedule availability failed to add.");
    _logger(error);
  };

  render() {
    return (
      <React.Fragment>
        <Formik
          enableReinitialize={true}
          initialValues={this.state.formData}
          validationSchema={dateValidationSchema}
          onSubmit={this.handleSubmit}
        >
          {(props) => {
            const {
              values,
              touched,
              errors,
              handleSubmit,
              isValid,
              isSubmitting,
            } = props;
            return (
              <Form onSubmit={handleSubmit} className="theme-form">
                <div className="schedule-form form-group p-50">
                  <div className="card">
                    <div className="card-header">
                      <h3>
                        <i className="fa fa-calendar"></i> Schedule Availability
                      </h3>
                    </div>
                    <div className="card-body">
                      <p>
                        Set a time period that you are available for a live
                        video chat.
                      </p>

                      <FormGroup className="row-form-group">
                        <Label
                          name="startTimeLabel"
                          htmlFor="startTimeLabel"
                          className="col-form-label pt-0"
                        >
                          Select a start date and time
                        </Label>
                        <Field
                          name="startTime"
                          type="datetime-local"
                          values={values.startTime}
                          autoComplete="off"
                          className={
                            errors.startTime && touched.startTime
                              ? "form-control is-invalid"
                              : "form-control"
                          }
                        />
                        {errors.startTime && touched.endTime && (
                          <span className="input-feedback">
                            {errors.startTime}
                          </span>
                        )}
                      </FormGroup>
                      <FormGroup className="row-form-group">
                        <Label
                          name="endTimeLabel"
                          htmlFor="endTimeLabel"
                          className="col-form-label pt-0"
                        >
                          Select an end date and time
                        </Label>
                        <Field
                          name="endTime"
                          type="datetime-local"
                          values={values.endTime}
                          autoComplete="off"
                          className={
                            errors.endTime && touched.endTime
                              ? "form-control is-invalid"
                              : "form-control"
                          }
                        />
                        {errors.endTime && touched.endTime && (
                          <span className="input-feedback">
                            {errors.endTime}
                          </span>
                        )}
                      </FormGroup>

                      <div className="card-footer">
                        <div className="schedule-button">
                          <div className="button p-t-20">
                            <Button
                              type="submit"
                              disabled={!isValid || isSubmitting}
                              className="btn btn-primary"
                            >
                              Submit
                            </Button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </Form>
            );
          }}
        </Formik>
      </React.Fragment>
    );
  }
}

EditScheduleAvailability.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func.isRequired,
  }),
  location: PropTypes.shape({
    pathname: PropTypes.string.isRequired,
    state: PropTypes.shape({
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
  }),
  match: PropTypes.shape({
    params: PropTypes.shape({
      id: PropTypes.string,
    }),
  }),
};

export default EditScheduleAvailability;
