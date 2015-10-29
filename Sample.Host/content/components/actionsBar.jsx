var ActionsBar = React.createClass({

  getInitialState() {
    return {
      permissionsShown: false
    };
  },

  render() {

    var disabled = this.props.hasSelection
      ? ""
      : " disabled";

    return (
      <div className={this.props.className}>
        <ul className="list-unstyled list-inline">
          <li><a href="#" className="btn btn-primary">Create User</a></li>
          <li><a href="#" className={"btn btn-default" + disabled} onClick={() => this.refs.permissionsDialog.open()}>Add Permission</a></li>
          <li><a href="#" className={"btn btn-default" + disabled}>Add Role</a></li>
        </ul>
        <PermissionsModal ref="permissionsDialog" />
      </div>
    );
  }

});
