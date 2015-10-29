var Modal = ReactBootstrap.Modal;
var Button = ReactBootstrap.Button;

var PermissionsModal = React.createClass({

  getInitialState() {
    return { showModal: this.props.intialState };
  },

  close() {
    this.setState({ showModal: false });
  },

  open() {
    this.setState({ showModal: true });
  },

  render() {
    return (
      <div className="static-modal">
        <Modal show={this.state.showModal} onHide={this.close}>
          <Modal.Header closeButton>
            <Modal.Title>Select Permissions</Modal.Title>
          </Modal.Header>

          <Modal.Body>
            One fine body...
          </Modal.Body>

          <Modal.Footer>
            <Button onClick={this.close}>Cancel</Button>
            <Button onClick={this.close} bsStyle="primary">Add</Button>
          </Modal.Footer>

        </Modal>
      </div>
    );
  }

});
