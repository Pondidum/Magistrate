import React from 'react'

import { Modal, Button, ProgressBar } from 'react-bootstrap'

const Dialog = React.createClass({

  getInitialState() {
    return {
      showModal: false,
      showAsync: false
    };
  },

  close() {
    this.setState({ showModal: false });
  },

  open() {
    this.setState({ showModal: true });
  },

  asyncStart() {
    this.setState({ showAsync: true });
  },

  asyncStop() {
    this.setState({ showAsync: false });
  },

  accept() {
    this.props.onSubmit();
    this.close();
  },

  render() {

    var async = this.state.showAsync
      ? (<img src="/img/loading.gif" style={{ marginRight: "1em" }} />)
      : null;

    var acceptStyle = this.props.acceptStyle || "primary";
    var acceptText = this.props.acceptText || "Add";

    return (
      <div className="static-modal">
        <Modal show={this.state.showModal} onHide={this.close} bsSize="lg">
          <Modal.Header closeButton>
            <Modal.Title>{this.props.title}</Modal.Title>
          </Modal.Header>

          <Modal.Body>
            {this.props.children}
          </Modal.Body>

          <Modal.Footer>
            {async}
            <Button onClick={this.accept} bsStyle={acceptStyle}>{acceptText}</Button>
            <Button onClick={this.close}>Cancel</Button>
          </Modal.Footer>

        </Modal>
      </div>
    );
  }

});

export default Dialog
