var ActionsBar = React.createClass({

  render() {

    var disabled = this.props.hasUsers
      ? ""
      : " disabled";

    return (
      <div className={this.props.className}>
        <ul className="list-unstyled list-inline">
          <li><a href="#" className="btn btn-primary">Create User</a></li>
          <li><a href="#" className={"btn btn-default" + disabled}>Add Permission</a></li>
          <li><a href="#" className={"btn btn-default" + disabled}>Add Role</a></li>
        </ul>
      </div>
    );
  }

});
