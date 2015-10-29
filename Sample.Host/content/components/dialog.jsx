var Modal = ReactBootstrap.Modal;
var Button = ReactBootstrap.Button;

var Dialog = React.createClass({

  getInitialState() {
    return { showModal: this.props.intialState };
  },

  close() {
    this.setState({ showModal: false });
  },

  open() {
    this.setState({ showModal: true });
  },

  accept() {
    this.close();
    this.props.onSubmit();
  },

  render() {
    return (
      <div className="static-modal">
        <Modal show={this.state.showModal} onHide={this.close}>
          <Modal.Header closeButton>
            <Modal.Title>{this.props.title}</Modal.Title>
          </Modal.Header>

          <Modal.Body>
            {this.props.children}
          </Modal.Body>

          <Modal.Footer>
            <Button onClick={this.close}>Cancel</Button>
            <Button onClick={this.accept} bsStyle="primary">Add</Button>
          </Modal.Footer>

        </Modal>
      </div>
    );
  }

});
