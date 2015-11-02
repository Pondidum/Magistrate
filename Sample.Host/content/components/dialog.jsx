var Modal = ReactBootstrap.Modal;
var Button = ReactBootstrap.Button;
var ProgressBar = ReactBootstrap.ProgressBar;

var Dialog = React.createClass({

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
  },

  render() {

    var async = this.state.showAsync
      ? (<img src="/img/loading.gif" style={{ marginRight: "1em" }} />)
      : null;

    var acceptStyle = this.props.acceptStyle || "primary";
    var acceptText = this.props.acceptText || "Add";
    var size = this.props.size || "large";

    return (
      <div className="static-modal">
        <Modal show={this.state.showModal} onHide={this.close} bsSize={size}>
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
